import "./grid.css";
import { GridHeader } from "./GridHeader";

export function Grid({ children }) {
    return (<>
        <div>Grid</div>
        <div className="grid" >
            {children}
        </div>
    </>
    );
}