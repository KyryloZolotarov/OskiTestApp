import React from 'react';
import {BrowserRouter as Router, Navigate, Route, Routes} from 'react-router-dom';
import {routes as routeConfig} from './routes';
import Layout from './components/Layout';
import {AuthProvider, useAuth} from './Auth/AuthProvider';

const App: React.FC = () => {
    return (
        <Router>
            <AuthProvider>
                <RoutesWithAuth/>
            </AuthProvider>
        </Router>
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
