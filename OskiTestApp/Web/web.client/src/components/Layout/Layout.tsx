import { ReactElement, FC, ReactNode } from "react";
import Navbar from "../NavBar/Navbar";

interface LayoutProps {
  children: ReactNode;
}

const Layout: FC<LayoutProps> = ({ children }): ReactElement => {
  return (
    <div>
      <div>{children}</div>
      <Navbar />
    </div>
  );
};

export default Layout;
