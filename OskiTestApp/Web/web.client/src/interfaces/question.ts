export interface IQuestion {
    id: number;
    question: string;
    answerVariants: { [key: string]: string };
    correctAnswersCount: number;
}