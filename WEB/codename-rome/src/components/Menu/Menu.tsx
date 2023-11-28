import { MenuItem } from "../../common/types/MenuItem";
import Product from "./Product/Product";
import "./Menu.scss";
import { useState } from "react";
import FormButton from "./FormButton/FormButton";
import Form from "./Form/Form";

interface MenuProp {
    menu: MenuItem[],
    addToMenu: (item: MenuItem) => void,
    deleteFromMenu: (id: string) => void,
    updateMenu: (item: MenuItem) => void
}

const Menu = ({menu, addToMenu, deleteFromMenu, updateMenu} : MenuProp) => {
    const defaultForm = {
        name: '',
        description: '',
        price: 0,
        category: '',
        ingredients: "PLACEHOLDER"
    }
    const [isFormVisible, setIsFormVisible] = useState(false)
    const [editItemForm, setEditItemForm] = useState(defaultForm)

    const toggleForm = () => {
        setEditItemForm(defaultForm)
        setIsFormVisible(!isFormVisible)
    }

    function openProductFormEditor (item: MenuItem){
        setIsFormVisible(!isFormVisible)
        setEditItemForm(item)
    }
    
    return (
        <div>
            <div className="main">
                <FormButton toggleForm = {toggleForm}/>
                {menu.map((item) => (
                    <Product key={item.name} product={item} deleteFromMenu={deleteFromMenu} openProductFormEditor={openProductFormEditor}/>
                ))}
            </div>
            {isFormVisible ? <Form onClose={toggleForm} addToMenu={addToMenu} updateMenu={updateMenu} editItemForm={editItemForm}/> : ""}
        </div>
    )
}

export default Menu;