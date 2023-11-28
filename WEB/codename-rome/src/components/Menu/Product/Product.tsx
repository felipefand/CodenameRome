import { useState } from "react";
import { MenuItem } from "../../../common/types/MenuItem";
import "./Product.scss";

interface ProductProps {
    product: MenuItem,
    deleteFromMenu: (id: string) => void,
    openProductFormEditor: (item: MenuItem) => void
}

const Product = ({product, deleteFromMenu, openProductFormEditor} : ProductProps) => {
    const [confirmingDelete, setConfirmingDelete] = useState(false)

    function clickDeleteButton(){
        setConfirmingDelete(true)
    }
    
    function cancelDelete(){
        setConfirmingDelete(false)
    }

    function confirmDelete(){
        if (product.id) deleteFromMenu(product.id)
    }

    return (
        <div className="card">
            <div className="product-title">{product.name}</div>
            <div className="product-description">{product.description}</div>
            <div className="product-price">$ {product.price}</div>
            <div className="product-buttons">
                <button onClick={() => product.id? openProductFormEditor(product) : ""}>update</button>
                <div>
                    {!confirmingDelete ? <button onClick={clickDeleteButton}>delete</button> : ""}
                    {confirmingDelete ? <button onClick={confirmDelete}>confirm</button> : ""}
                    {confirmingDelete ? <button onClick={cancelDelete}>cancel</button> : ""}
                </div>
            </div>
        </div>
    )
}

export default Product;