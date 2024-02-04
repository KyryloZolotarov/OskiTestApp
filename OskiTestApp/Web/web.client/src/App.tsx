import { useEffect, useState } from 'react';
import AuthComponent from './Auth/AuthComponent'; 
import './App.css';

interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

function App() {
    const [forecasts, setForecasts] = useState<Forecast[]>();
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    useEffect(() => {
        // Проверка аутентификации пользователя
        // Предположим, что у вас есть метод для проверки аутентификации:
        checkAuthStatus();
    }, []);

    const checkAuthStatus = async () => {
        // Здесь должен быть код для проверки аутентификации
        // Если пользователь аутентифицирован, установите isLoggedIn в true
        setIsLoggedIn(true); // Это только для примера

        if (isLoggedIn) {
            await populateWeatherData(); // Загрузка данных о погоде, если пользователь аутентифицирован
        }
    };

    const contents = !isLoggedIn
    ? <p><em>Please log in to view the weather forecast.</em></p> // Сообщение для неаутентифицированных пользователей
    : forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
          </table>;

    return (
        <div>
            <AuthComponent onLogin={() => {setIsLoggedIn(true); populateWeatherData()}} onLogout={() => setIsLoggedIn(false)} isLoggedIn={isLoggedIn} />
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWeatherData() {
        const response = await fetch('http://localhost:5000/weatherforecast', {
            credentials: 'include' // Убедитесь, что куки отправляются с запросом
        });
        const data = await response.json();
        setForecasts(data);
    }
}

export default App;