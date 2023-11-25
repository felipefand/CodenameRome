import { useState } from "react";
import { MenuItem } from "../../../common/types/MenuItem";

interface FormProps {
    isOpen: boolean;
    onClose: () => void
    addToMenu: (menuItem: MenuItem) => void
}

const Form: React.FC<FormProps> = ({isOpen, onClose, addToMenu} : FormProps) => {
    const [formState, setForm] = useState({
        name: '',
        description: '',
        price: 0,
        category: '',
        ingredients: "PLACEHOLDER"
    })

    function postForm(event: React.FormEvent) {
        event.preventDefault()
        addToMenu(formState)
        setForm({
            name: '',
            description: '',
            price: 0,
            category: '',
            ingredients: "PLACEHOLDER"
        })
    }

    if (isOpen)
    return (
        <div>
            <form onSubmit={postForm}>
                Product Name: <input value={formState.name} type="text" onChange={event => setForm({...formState, name: event.target.value})} />
                <br/>
                Product Description: <input value={formState.description} type="text" onChange={event => setForm({...formState, description: event.target.value})}/>
                <br/>
                Product Price: <input value={formState.price} type="text" onChange={event => setForm({...formState, price: Number(event.target.value)})}/>
                <br/>
                Product Category: <input value={formState.category} type="text" onChange={event => setForm({...formState, category: event.target.value})}/>
                <br/>
                <button type="submit">Add Product</button>
            </form>
        </div>
    )
    
    else return null
}

export default Form;