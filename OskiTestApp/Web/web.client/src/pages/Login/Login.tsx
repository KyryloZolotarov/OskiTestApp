import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { useAuth } from "../../Auth/AuthProvider";
import { Typography, TextField, Button, Container } from '@mui/material';

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
                "http://localhost:5003/auth/signup",
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
                setAccountExist(true);
            }
        } catch (error) {
            console.error("Sign Up failed", error);
        }
    };

    const handleLogin = async (): Promise<void> => {
        try {
            const response = await axios.post(
                "http://localhost:5003/auth/login",
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
            }
        } catch (error) {
            console.error("Login failed", error);
        }
    };

    const handleLogout = async (): Promise<void> => {
        try {
            const response = await axios.post(
                "http://localhost:5003/auth/logout",
                {},
                {
                    withCredentials: true,
                }
            );

            if (response.status === 200) {
                console.log("Logout successful");
                onLogout();
                setAccountExist(accountExist);
                navigate("/login");
            }
        } catch (error) {
            console.error("Logout failed", error);
        }
    };

    return (
        <Container maxWidth="sm" style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
            <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '20px' }}>
                {localAccountExist ? (
                    <>
                        <Typography variant="h6"><em>Please log in to start</em></Typography>
                        <TextField
                            type="text"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            placeholder="Email"
                            style={{ backgroundColor: '#FFFFFF' }} // светлый фон
                        />
                        <TextField
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            placeholder="Password"
                            style={{ backgroundColor: '#FFFFFF' }} // светлый фон
                        />
                        <div style={{ display: "flex", gap: "10px" }}>
                        <Button onClick={handleLogin} variant="contained" style={{ backgroundColor: '#284d66', color: '#FFFFFF' }}>Login</Button>
                        <Button onClick={handleSignUp} variant="contained" style={{ backgroundColor: '#284d66', color: '#FFFFFF' }}>Sign Up</Button>
                        </div>                        
                    </>
                ) : (
                    <>
                        <TextField
                            type="text"
                            value={firstName}
                            onChange={(e) => setFirstName(e.target.value)}
                            placeholder="First Name"
                            style={{ backgroundColor: '#FFFFFF' }} // светлый фон
                        />
                        <TextField
                            type="text"
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
                            placeholder="Last Name"
                            style={{ backgroundColor: '#FFFFFF' }} // светлый фон
                        />
                        <TextField
                            type="text"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            placeholder="Email"
                            style={{ backgroundColor: '#FFFFFF' }} // светлый фон
                        />
                        <TextField
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            placeholder="Password"
                            style={{ backgroundColor: '#FFFFFF' }} // светлый фон
                        />
                        <Button onClick={register} variant="contained" style={{ backgroundColor: '#284d66', color: '#FFFFFF' }}>Sign Up</Button> {/* темный фон, белый текст */}
                    </>
                )}
            </div>
        </Container>
    );
};

export default Login;