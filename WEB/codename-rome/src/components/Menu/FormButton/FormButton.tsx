import "../Product/Product.scss"
import { CgAddR } from "react-icons/cg"

interface FormButtonProps {
    toggleForm: () => void
}

const FormButton = ({toggleForm} : FormButtonProps) => {
    return (
        <div onClick={toggleForm} className="card plus-card">
            <CgAddR size={70}/>
        </div>
    )
}

export default FormButton;