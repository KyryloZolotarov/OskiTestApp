export interface IQuestion {
    Id: number;
    Question: string;
    AnswerVariants: { [key: number]: string };
    CorrectAnswers: number[];
}