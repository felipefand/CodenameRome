import { useEffect, useState } from "react";
import { MenuItem } from "../../../common/types/MenuItem";

interface FormProps {
    onClose: () => void
    addToMenu: (menuItem: MenuItem) => void
    editItemForm: MenuItem
    updateMenu: (item: MenuItem) => void
}

const Form: React.FC<FormProps> = ({onClose, addToMenu, editItemForm, updateMenu} : FormProps) => {
    const [formState, setForm] = useState(editItemForm)
    const isEditing = editItemForm.id? true : false

    function postForm() {
        addToMenu(formState)
        setForm({
            name: '',
            description: '',
            price: 0,
            category: '',
            ingredients: "PLACEHOLDER"
        })
    }

    function putForm(){
        updateMenu(formState)
        onClose()
    }

    function submitForm(){
        if (isEditing) putForm()
        else postForm()
    }

    return (
        <div>
            <form>
                Product Name: <input value={formState.name} type="text" onChange={event => setForm({...formState, name: event.target.value})} />
                <br/>
                Product Description: <input value={formState.description} type="text" onChange={event => setForm({...formState, description: event.target.value})}/>
                <br/>
                Product Price: <input value={formState.price} type="text" onChange={event => setForm({...formState, price: Number(event.target.value)})}/>
                <br/>
                Product Category: <input value={formState.category} type="text" onChange={event => setForm({...formState, category: event.target.value})}/>
                <br/>
                <button onClick={submitForm} type="button">{isEditing? "Update Product" : "Add Product"}</button>
            </form>
        </div>
    )
}

export default Form;