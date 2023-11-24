import { MenuItem } from "../../common/types/MenuItem";
import Product from "./Product/Product";

interface MenuProp {
    menu: MenuItem[]
}

const Menu = (menuProp : MenuProp) => {
    return (
        <div>
            {menuProp.menu.map((item) => (
                <Product key={item.name} {...item}/>
            ))}
        </div>
    )
}

export default Menu;