import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

interface TestData {
  Names: {
    [key: number]: string;
  };
}

const Home = () => {
  const [testsData, setTestsData] = useState<TestData | null>(null);
  const [selectedTest, setSelectedTest] = useState<number | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    // Запрос к серверу для получения реальных данных
    fetch('/api/tests')
      .then((response) => response.json())
      .then((data: TestData) => setTestsData(data))
      .catch((error) => console.error('Ошибка при загрузке данных', error));
  }, []);

  const handleTestSelect = (testId: number) => {
    setSelectedTest(testId);
    navigate(`/tests/${testId}`);
  };

  return (
    <div>
      <h1>Список тестов</h1>
      {testsData ? (
        <ul>
          {Object.keys(testsData.Names).map((testId) => (
            <li key={testId}>
              <button onClick={() => handleTestSelect(Number(testId))}>
                {testsData.Names[Number(testId)]}
              </button>
            </li>
          ))}
        </ul>
      ) : (
        <p>Загрузка данных...</p>
      )}

      {selectedTest && testsData && (
        <div>
          <h2>Выбранный тест: {testsData.Names[selectedTest]}</h2>
          {/* Здесь можно добавить дополнительную информацию о выбранном тесте */}
        </div>
      )}
    </div>
  );
};

export default Home;