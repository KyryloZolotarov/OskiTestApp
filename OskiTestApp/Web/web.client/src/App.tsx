import React from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import { routes as routeConfig } from './routes';
import Layout from './components/Layout';
import { AuthProvider, useAuth } from './Auth/AuthProvider';

const App: React.FC = () => {
  const { isAuthenticated } = useAuth();

  return (
    <AuthProvider>
      <Routes>
        {routeConfig.map((route) => (
          <Route
            key={route.key}
            path={route.path}
            element={
              route.protected && !isAuthenticated ? (
                <Navigate to="/login" />
              ) : (
                <Layout>{<route.component />}</Layout>
              )
            }
          />
        ))}
      </Routes>
    </AuthProvider>
  );
};

export default App;