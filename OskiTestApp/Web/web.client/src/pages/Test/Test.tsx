import React, {ReactElement, FC, useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";

interface TestDto {
  Id: number;
  Name: string;
  Description: string;
  Questions: QuestionDto[];
}

interface QuestionDto {
  Id: number;
  Text: string;
  // Другие свойства вашего объекта QuestionDto
}

const YourComponent = () => {
  const [data, setData] = useState<TestDto | null>(null);

// ...

  const result: TestDto = await response.json();
  setData(result);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('your-api-endpoint');
        const result = await response.json();
        setData(result);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
      {data && (
        <div>
          <p>ID: {data.Id}</p>
          <p>Name: {data.Name}</p>
          <p>Description: {data.Description}</p>
          <ul>
            {data.Questions.map((question) => (
              <li key={question.Id}>{question.Text}</li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
};

export default Test;