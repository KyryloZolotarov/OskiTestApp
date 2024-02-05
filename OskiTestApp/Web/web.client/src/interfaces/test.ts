import { IQuestion } from "./question";
export interface ITest {
    Id: number;
    Name: string;
    Description: string;
    Questions: IQuestion[];
}