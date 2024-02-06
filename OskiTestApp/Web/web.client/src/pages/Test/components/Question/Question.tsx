import React from 'react';
import {IQuestion} from '../../../../interfaces/question';
import Answer from '../Answer/Answer';

interface QuestionProps {
    question: IQuestion;
    onAnswerChange: (questionId: number, answerId: number) => void;
}

const Question: React.FC<QuestionProps> = ({question, onAnswerChange}) => {
    return (
        <div>
            <p>{question.question}</p>
            {Object.entries(question.answerVariants).map(([answerId, answerText]) => (
                <Answer
                    key={answerId}
                    answerId={answerId}
                    answerText={answerText}
                    onAnswerChange={() => onAnswerChange(Number(question.id), Number(answerId))}
                />
            ))}
        </div>
    );
};

export default Question;