import { useState, useEffect } from 'react';

interface PassedTest {
  id: number;
  name: string;
  mark: number;
}

const PassedTests = () => {
  const [tests, setTests] = useState<PassedTest[]>([]);

  useEffect(() => {
    // Function to fetch data from the API
    const fetchData = async () => {
      try {
        const response = await fetch('YOUR_API_ENDPOINT'); // Replace with your actual API endpoint
        const data: PassedTest[] = await response.json();
        setTests(data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    // Call the fetch data function
    fetchData();
  }, []);

  return (
    <div>
      <h1>Passed Tests</h1>
      <ul>
        {tests.map((test) => (
          <li key={test.id}>
            <strong>Name:</strong> {test.name}, <strong>Mark:</strong> {test.mark}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default PassedTests;