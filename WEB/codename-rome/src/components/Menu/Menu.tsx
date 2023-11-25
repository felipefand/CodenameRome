import { MenuItem } from "../../common/types/MenuItem";
import Product from "./Product/Product";
import "./Menu.scss";
import { useState } from "react";
import FormButton from "./FormButton/FormButton";
import Form from "./Form/Form";

interface MenuProp {
    menu: MenuItem[],
    addToMenu: (item: MenuItem) => void,
    deleteFromMenu: (id: string) => void
}

const Menu = ({menu, addToMenu, deleteFromMenu} : MenuProp) => {
    const [isFormVisible, setIsFormVisible] = useState(false)

    const toggleForm = () => {
        setIsFormVisible(!isFormVisible)
    }
    
    return (
        <div>
            <div className="main">
                <FormButton toggleForm = {toggleForm}/>
                {menu.map((item) => (
                    <Product key={item.name} product={item} deleteFromMenu={deleteFromMenu}/>
                ))}
            </div>
            <Form isOpen={isFormVisible} onClose={toggleForm} addToMenu={addToMenu}/>
        </div>
    )
}

export default Menu;