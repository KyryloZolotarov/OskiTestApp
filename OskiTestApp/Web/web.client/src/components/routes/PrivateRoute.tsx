import React, { ReactNode } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../Auth/AuthProvider';
type PrivateRouteProps = {
  children: ReactNode;
};

export const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
  const { isAuthenticated } = useAuth();
  const location = useLocation();

  if (!isAuthenticated) {
    // Перенаправление на страницу логина, если пользователь не аутентифицирован
    return <Navigate to="/login" state={{ from: location }} />;
  }

  return <>{children}</>; // Обернуто в React.Fragment для явности
};