import {FC, ReactElement, ReactNode} from "react";
import Navbar from "../NavBar/Navbar";

interface LayoutProps {
    children: ReactNode;
}

const Layout: FC<LayoutProps> = ({children}): ReactElement => {
    return (
        <div>
            <Navbar/>
            <div>{children}</div>
        </div>
    );
};

export default Layout;
