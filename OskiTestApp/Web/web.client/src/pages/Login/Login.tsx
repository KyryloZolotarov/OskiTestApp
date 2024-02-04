import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { useAuth } from "../../Auth/AuthProvider";

interface Authorization {
  onLogin: () => void;
  onLogout: () => void;
  accountExist: boolean;
}

const Login: React.FC<Authorization> = ({
  onLogin,
  onLogout,
  accountExist,
}) => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [localAccountExist, setAccountExist] = useState<boolean>(true);
  const navigate = useNavigate();
  const { setIsAuthenticated } = useAuth();

  const handleSignUp = async () => {
    setAccountExist(false);
  };

  const register = async () => {
    try {
      const response = await axios.post(
        "http://localhost:5000/auth/signup",
        {
          email,
          firstName,
          lastName,
          password,
        },
        {
          withCredentials: true,
        }
      );

      if (response.status === 200) {
        console.log("Sign up successful");
        onLogin();
        navigate(`/`);
        // Перенаправить пользователя или изменить состояние аутентификации
      }
    } catch (error) {
      console.error("Sign Up failed", error);
      // Обработать ошибку логина
    }
  };

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
        setIsAuthenticated(true);
        navigate(`/`);
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
        setAccountExist(accountExist);
        navigate("/");
        // Перенаправить пользователя или изменить состояние аутентификации
      }
    } catch (error) {
      console.error("Logout failed", error);
      // Обработать ошибку логаута
    }
  };

  return (
    <div>
      {localAccountExist ? (
        <>
          <p>
            <em>Please log in to start</em>
          </p>
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
          <button onClick={handleSignUp}>Sign Up</button>
        </>
      ) : (
        <>
          <input
            type="text"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            placeholder="First Name"
          />
          <input
            type="text"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            placeholder="Last Name"
          />
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
          <button onClick={register}>Sign Up</button>
        </>
      )}
    </div>
  );
};

export default Login;
