import React from 'react';
import { IQuestion } from '../../../../interfaces/question';
import Answer from '../Answer/Answer';

interface QuestionProps {
  question: IQuestion;
  onAnswerChange: (questionId: number, answerId: number) => void;
}

const Question: React.FC<QuestionProps> = ({ question, onAnswerChange }) => {
  return (
    <div>
      <p>{question.Question}</p>
      {Object.entries(question.AnswerVariants).map(([answerId, answerText]) => (
        <Answer
          key={answerId}
          answerId={answerId}
          answerText={answerText}
          onAnswerChange={() => onAnswerChange(Number(question.Id), Number(answerId))}
        />
      ))}
    </div>
  );
};

export default Question;