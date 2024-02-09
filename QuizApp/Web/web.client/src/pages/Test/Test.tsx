import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Question from "./components/Question";
import { Typography, Button, Box } from "@mui/material";
import { useNavigate } from "react-router-dom";

const Test = () => {
  const [questions, setQuestions] = useState<any[]>([]); // Adjust the type as needed
  const [answers, setAnswers] = useState<Record<string, number[]>>({});
  const { testId } = useParams<{ testId?: string }>(); // Mark testId as optional
  const navigate = useNavigate();

  const handleAnswerChange = (questionId: string, answerId: number) => {
    setAnswers((prevAnswers) => {
      const updatedAnswers = { ...prevAnswers };
      const selectedAnswers = updatedAnswers[questionId] || [];
      const answerIndex = selectedAnswers.indexOf(answerId);
      if (answerIndex === -1) {
        updatedAnswers[questionId] = [...selectedAnswers, answerId];
      } else {
        updatedAnswers[questionId] = selectedAnswers.filter((id) => id !== answerId);
      }
      return updatedAnswers;
    });
  };

  const handleCancelTest = () => {
    navigate('/');
  };

  const handleSubmit = () => {
    const complitedTest = {
      testId: testId ? parseInt(testId) : undefined,
      answers: Object.entries(answers).map(([questionId, selectedAnswers]) => ({
        QuiestionId: parseInt(questionId),
        AnswersKeys: selectedAnswers.map(String)
      }))
    };

    fetch('http://localhost:5003/test/submitAnswers', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      credentials: "include",
      body: JSON.stringify(complitedTest)
    })
    .then(response => {
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      
    navigate(`/passedtests`);
    })
    .then(data => {
      console.log('Response from server:', data);
      // Handle response from server if needed
    })
    .catch(error => {
      console.error('There was an error with the fetch operation:', error);
      // Handle error if needed
    });
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        if (!testId) return; // Exit early if testId is undefined
        const response = await fetch(
          `http://localhost:5003/test/getTest?testId=${testId}`
        );
        const result = await response.json();
        setQuestions(result.questions || []);
      } catch (error) {
        console.error("Ошибка при получении данных:", error);
        setQuestions([]);
      }
    };

    fetchData();
  }, [testId]);

  return (
    <div>
      <Typography variant="h2" gutterBottom>
        {/* Display test name or other relevant information */}
      </Typography>
      <Box sx={{ paddingLeft: 2 }}>
        {questions.map((question: any) => ( // Adjust the type as needed
          <Question
            key={question.id} // Ensure 'id' property exists or adjust accordingly
            question={question}
            onAnswerChange={(questionId, answerId) => handleAnswerChange(String(questionId), answerId)} // Ensure 'questionId' is treated as a string
          />
        ))}
      </Box>
      <Box sx={{ display: "flex", justifyContent: "center", paddingBottom: 2 }}>
        <Box sx={{ width: '50%', marginRight: 1 }} />
            <Button variant="contained" onClick={handleCancelTest} style={{ backgroundColor: '#284d66', color: '#FFFFFF' }} sx={{ width: '50%', marginRight: 1 }}>
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