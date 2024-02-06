import {IQuestion} from "./question";

export interface ITest {
    id: number;
    name: string;
    description: string;
    questions: IQuestion[];
}