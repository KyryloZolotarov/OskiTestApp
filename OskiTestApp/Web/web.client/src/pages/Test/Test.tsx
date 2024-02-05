import {useEffect, useState} from "react";
import {useLocation, useNavigate} from "react-router-dom";
import { ITest } from "../../interfaces/test";
import { IQuestion } from "../../interfaces/question";
import Question from "./components/Question";

const Test= () => {
  const location = useLocation();
  const testId = location.pathname.split('/').pop(); // Получение ID из пути
  const [questions, setQuestions] = useState<IQuestion[]>([]);
  const [answers, setAnswers] = useState({});

  const handleAnswerChange = (questionId: number, answerId: number) => {
    setAnswers((prevAnswers) => ({ ...prevAnswers, [questionId]: answerId }));
  };

  const handleSubmit = () => {
    // Send answers to the server
    console.log('Submitting answers:', answers);
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`http://localhost:5003/test/gettest?id=${testId}`);
        const result: ITest = await response.json();
        setQuestions(result.Questions);
      } catch (error) {
        console.error('Ошибка при получении данных:', error);
      }
    };

    fetchData();
  }, [testId]);

  return (
    <div>
      <h2>{/* Change to questions[0]?.Name or something similar */}</h2>
      {questions.map((question) => (
        <Question
          key={question.Id}
          question={question}
          onAnswerChange={handleAnswerChange}
        />
      ))}
      <button onClick={handleSubmit}>Submit Answers</button>
    </div>
  );
};

export default Test;