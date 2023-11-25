import { Link } from "react-router-dom"
import SidebarData from "../../common/sidebardata/sidebardata"

const Navbar = () => {
    return (
        <div>
        {SidebarData.map((item, index) => (
            <li key = {index}>
                <Link to={item.path}>
                    <span>{item.title}</span>
                </Link>
            </li>
        ))}
        </div>
    )
};

export default Navbar;