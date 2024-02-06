import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ITest } from "../../interfaces/test";
import { IQuestion } from "../../interfaces/question";
import Question from "./components/Question";
import { Typography, Button, Box } from "@mui/material";
import { useNavigate } from "react-router-dom";

const Test = () => {
  const [questions, setQuestions] = useState<IQuestion[]>([]);
  const [answers, setAnswers] = useState({});
  const { testId } = useParams();
  const navigate = useNavigate();

  const handleAnswerChange = (questionId: number, answerId: number) => {
    setAnswers((prevAnswers) => ({ ...prevAnswers, [questionId]: answerId }));
  };

  const hanelCanselTest = () => {
    navigate('/')
  }

  const handleSubmit = () => {
    // Send answers to the server
    console.log("Submitting answers:", answers);
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(
          `http://localhost:5003/test/getTest?testId=${testId}`
        );
        const result: ITest = await response.json();

        // Check if result.Questions is an array before setting it to state
        if (Array.isArray(result.questions)) {
          setQuestions(result.questions);
        } else {
          // If result.Questions is not an array, log an error and set questions to an empty array
          console.error("Received data is not in the expected format:", result);
          setQuestions([]);
        }
      } catch (error) {
        console.error("Ошибка при получении данных:", error);
        // You can also set questions to an empty array in case of an error if it makes sense for your app
        setQuestions([]);
      }
    };

    fetchData();
  }, [testId]);

  return (
    <div>
      <Typography variant="h2" gutterBottom>
        {/* Change to questions[0]?.Name or something similar */}
      </Typography>
      <Box sx={{ paddingLeft: 2 }}>
        {questions &&
          questions.length > 0 &&
          questions.map((question) => (
            <Question
              key={question.id}
              question={question}
              onAnswerChange={handleAnswerChange}
            />
          ))}
      </Box>
      <Box sx={{ display: "flex", justifyContent: "center", paddingBottom: 2 }}>
        <Box sx={{ width: '50%', marginRight: 1 }} />
            <Button variant="contained" onClick={hanelCanselTest} style={{ backgroundColor: '#284d66', color: '#FFFFFF' }} sx={{ width: '50%', marginRight: 1 }}>
            Cancel Test
            </Button>
            <Button variant="contained" onClick={handleSubmit} style={{ backgroundColor: '#284d66', color: '#FFFFFF' }} sx={{ width: '50%', marginLeft: 1 }}>
            Submit Answers
            </Button>
        <Box sx={{ width: '50%', marginLeft: 1 }} />
    </Box>
    </div>
  );
};

export default Test;