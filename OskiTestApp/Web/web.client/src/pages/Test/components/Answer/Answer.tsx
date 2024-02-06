import React from 'react';
import { FormControl, FormGroup, FormControlLabel, Checkbox, Box } from '@mui/material';

interface AnswerProps {
    answerId: string;
    answerText: string;
    onAnswerChange: () => void;
}

const Answer: React.FC<AnswerProps> = ({ answerId, answerText, onAnswerChange }) => {
    return (
        <Box marginBottom={1}>
            <FormControl component="fieldset">
                <FormGroup>
                    <FormControlLabel
                        control={
                            <Checkbox
                                color="success"
                                onChange={onAnswerChange}
                                id={answerId}
                                name={`answer-${answerId}`}
                            />
                        }
                        label={answerText}
                    />
                </FormGroup>
            </FormControl>
        </Box>
    );
};

export default Answer;