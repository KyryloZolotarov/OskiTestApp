import React from 'react';
import { IQuestion } from '../../../../interfaces/question';
import Answer from '../Answer/Answer';
import { Typography, Box } from '@mui/material';

interface QuestionProps {
    question: IQuestion;
    onAnswerChange: (questionId: number, answerId: number) => void;
}

const Question: React.FC<QuestionProps> = ({ question, onAnswerChange }) => {
    return (
        <Box marginBottom={2}>
            <Typography variant="body1" gutterBottom>
                {question.question}
            </Typography>
            {Object.entries(question.answerVariants).map(([answerId, answerText]) => (
                <Answer
                    key={String(answerId)} // Преобразуем числовой ключ в строку
                    answerId={String(answerId)}
                    answerText={answerText}
                    onAnswerChange={() => onAnswerChange(Number(question.id), Number(answerId))}
                />
            ))}
        </Box>
    );
};

export default Question;