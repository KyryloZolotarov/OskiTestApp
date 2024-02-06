import React from 'react';
import {BrowserRouter as Router, Navigate, Route, Routes} from 'react-router-dom';
import { CssBaseline, ThemeProvider } from "@mui/material";
import { createTheme } from "@mui/material/styles";
import {routes as routeConfig} from './routes';
import Layout from './components/Layout';
import {AuthProvider, useAuth} from './Auth/AuthProvider';

const theme = createTheme({
    palette: {
      primary: {
        light: "#63b8ff",
        main: "#3b444a",
        dark: "#005db0",
        contrastText: "#000",
      },
      secondary: {
        main: "#4db6ac",
        light: "#82e9de",
        dark: "#00867d",
        contrastText: "#000",
      },
    },
  });

const App: React.FC = () => {
    return (
        <ThemeProvider theme={theme}>
            <div style={{ backgroundColor: theme.palette.primary.main, minHeight: '100vh' }}>
            <CssBaseline />
                    <Router>
                        <AuthProvider>
                            <RoutesWithAuth/>
                        </AuthProvider>
                    </Router> 
            </div>       
        </ThemeProvider>
    );
};

const RoutesWithAuth = () => {
    const {isAuthenticated} = useAuth();

    return (        
            <Routes>
                {routeConfig.map((route) => (
                    <Route
                        key={route.key}
                        path={route.path}
                        element={
                            route.protected && !isAuthenticated ? (
                                <Navigate to="/login"/>
                            ) : route.protected ? (
                                <Layout>{React.createElement(route.component)}</Layout>
                            ) : (
                                React.createElement(route.component)
                            )
                        }
                    />
                ))}
            </Routes>
    );
};

export default App;
