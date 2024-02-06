import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";

interface TestData {
    names: {
        [key: number]: string;
    };
}

const Home = () => {
    const [testsData, setTestsData] = useState<TestData | null>(null);
    const [selectedTest, setSelectedTest] = useState<number | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        // Запрос к серверу для получения реальных данных
        fetch("http://localhost:5003/test/getAvailableTests", {
            method: "GET", // or 'POST'
            headers: {
                "Content-Type": "application/json",
                // other headers can go here
            },
            credentials: "include", // Important: this will send cookies with your request
        })
            .then((response) => response.json())
            .then((data) => setTestsData(data)) // Assuming setTestsData is your state setter
            .catch((error) => console.error("Ошибка при загрузке данных", error));
    }, []);

    const handleTestSelect = (testId: number) => {
        setSelectedTest(testId);
        navigate(`/test/${testId}`);
    };

    return (
        <div>
            <h1>Список тестов</h1>
            {testsData ? (
                <ul>
                    {testsData &&
                        testsData.names &&
                        Object.keys(testsData.names).map((testId) => (
                            <li key={testId}>
                                <button onClick={() => handleTestSelect(Number(testId))}>
                                    {testsData.names[testId]}
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
