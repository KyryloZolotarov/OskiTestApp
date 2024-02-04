import React, { useState } from "react";
import axios from "axios";

interface AuthComponentProps {
    onLogin: () => void;
    onLogout: () => void;
    isLoggedIn: boolean;
}

const AuthComponent: React.FC<AuthComponentProps> =  ({ onLogin, onLogout, isLoggedIn }) => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  const handleLogin = async (): Promise<void> => {
    try {
      const response = await axios.post(
        "http://localhost:5000/auth/login",
        {
          email,
          password,
        },
        {
          withCredentials: true,
        }
      );

      if (response.status === 200) {
        console.log("Login successful");
        onLogin();
        // Перенаправить пользователя или изменить состояние аутентификации
      }
    } catch (error) {
      console.error("Login failed", error);
      // Обработать ошибку логина
    }
  };

  const handleLogout = async (): Promise<void> => {
    try {
      const response = await axios.post(
        "http://localhost:5000/auth/logout",
        {},
        {
          withCredentials: true,
        }
      );

      if (response.status === 200) {
        console.log("Logout successful");
        onLogout();
        // Перенаправить пользователя или изменить состояние аутентификации
      }
    } catch (error) {
      console.error("Logout failed", error);
      // Обработать ошибку логаута
    }
  };

  return (
    <div>
      {!isLoggedIn ? (
        <>
          <input
            type="text"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="Email"
          />
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Password"
          />
          <button onClick={handleLogin}>Login</button>
        </>
      ) : (
        <button onClick={handleLogout}>Logout</button>
      )}
    </div>
  );
};

export default AuthComponent;

