import { useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { routes as appRoutes } from "./routes";
import Login from "./pages/Login/Login";
import Layout from "./components/Layout/Layout"; 
import './App.css';

function App() {
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
    };

    const contents = !isLoggedIn
    ? <p><em>Please log in to start</em></p> // Сообщение для неаутентифицированных пользователей
    : 
    <div>
            <Router>
                <Layout>
                    <Routes>
                        {appRoutes.map((route) => (
                            <Route
                                key={route.key}
                                path={route.path}
                                element={<route.component />}
                            />
                        ))}
                    </Routes>
                </Layout>
            </Router>
    </div>

    return (
        <div>
            <Login onLogin={() => {setIsLoggedIn(true)}} onLogout={() => setIsLoggedIn(false)} accountExist={accountExist} />
            {contents}
        </div>
    );
}

export default App;