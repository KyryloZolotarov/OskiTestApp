import React from 'react';

interface AnswerProps {
    answerId: string;
    answerText: string;
    onAnswerChange: () => void; // Adjust the type according to your needs
  }
  
  const Answer: React.FC<AnswerProps> = ({ answerId, answerText, onAnswerChange }) => {
    return (
      <div>
        <input
          type="radio"
          id={answerId}
          name={`answer-${answerId}`}
          onChange={onAnswerChange}
        />
        <label htmlFor={answerId}>{answerText}</label>
      </div>
    );
  };

  export default Answer;