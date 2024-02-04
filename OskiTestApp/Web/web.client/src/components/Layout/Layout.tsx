import React, { ReactElement, FC, ReactNode } from "react";
import Navbar from "../NavBar/Navbar";
import { useAuth } from "../../Auth/AuthProvider";

interface LayoutProps {
  children: ReactNode;
}

const Layout: FC<LayoutProps> = ({ children }): ReactElement => {
  const { isAuthenticated } = useAuth();
  return (
    <div>
      {!isAuthenticated ? (
        <div>{children}</div>
      ) : (
        <div>
          {children}
          <Navbar />
        </div>
      )}
    </div>
  );
};

export default Layout;
