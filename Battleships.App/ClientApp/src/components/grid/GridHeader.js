import "./grid.css";

export function GridHeader({ header, className }) {
    return (
        <div className={`${className} grid-header`}>
            {header}
        </div>
    )
}