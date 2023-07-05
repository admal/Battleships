import "./grid.css";
import { GridHeader } from "./GridHeader";

export function Grid({ children, sizeX, sizeY }) {
    const nums = Array.from({length: sizeX}, (val, idx) => idx);
    console.log(nums);  
    return (<>
        <div>Grid</div>
        <div className="grid" >
            {/*<GridHeader header=" " />*/}
            {/*{*/}
            {/*    nums.map(x => { return (<GridHeader key={x} header={x} />) })*/}
            {/*    // Array.from(Array(sizeX)).map(x => { (<GridHeader key={x} header={x} />) })*/}
            {/*}*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="1" />*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="2" />*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="3" />*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="4" />*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="5" />*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="6" />*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="7" />*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="8" />*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="9" />*/}
            {/*<GridHeader style={{"gridArea": "header-left"}} header="10" />*/}
            {children}

        </div>
    </>
    );
}