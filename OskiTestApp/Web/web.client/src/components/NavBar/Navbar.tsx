import {useAuth} from "../../Auth/AuthProvider";
import {Link} from "react-router-dom";

const Navbar = () => {
    const {logout} = useAuth();

    const handleLogout = () => {
        logout();
        // Дополнительные действия при выходе
    };

    return (
        <div className="navbar">
            {/* Ваша навигационная панель и кнопка выхода */}
            <ul className="nav-buttons">
                <li>
                    <Link to="/passedtests">Passed Tests</Link>
                </li>
                <li>
                    <Link to="/tests">Tests</Link>
                </li>
                <li>
                    <button onClick={handleLogout} className="logout-button">
                        Logout
                    </button>
                </li>
            </ul>
        </div>
    );
};

export default Navbar;
