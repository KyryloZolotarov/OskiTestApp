import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import {ITest} from "../../interfaces/test";
import {IQuestion} from "../../interfaces/question";
import Question from "./components/Question";

const Test = () => {
    const [questions, setQuestions] = useState<IQuestion[]>([]);
    const [answers, setAnswers] = useState({});
    const {testId} = useParams();
    const handleAnswerChange = (questionId: number, answerId: number) => {
        setAnswers((prevAnswers) => ({...prevAnswers, [questionId]: answerId}));
    };

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
            <h2>{/* Change to questions[0]?.Name or something similar */}</h2>
            {questions &&
                questions.length > 0 &&
                questions.map((question) => (
                    <Question
                        key={question.id}
                        question={question}
                        onAnswerChange={handleAnswerChange}
                    />
                ))}
            <button onClick={handleSubmit}>Submit Answers</button>
        </div>
    );
};

export default Test;
