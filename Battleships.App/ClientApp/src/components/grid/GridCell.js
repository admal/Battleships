import { useState } from 'react';
import "./grid.css";

export function GridCell({  gameGuid, x, y, onGameFinished, onShipSunk }) {
    const [status, setStatus] = useState("water");
    async function hitCell() {
        if (status !== "water") {
            return;
        }

        const response = await fetch(`api/game/hit/${gameGuid}/${x}/${y}`, {
            method: "PUT"
        });

        if (!response.ok) {
            throw new Error(`Something went wrong, couldn't hit cell (${x}, ${y})`);
        }

        const result = await response.json();

        if (result.hitResult === 0) {
            setStatus("ship-hit");
        } else {
            setStatus("missed-hit")
        }

        if (result.shipSunk) {
            onShipSunk(result.shipSunk);
        }

        if (result.gameFinished) {
            onGameFinished();
        }

    }

    return (
        <div className={`battlefield grid-cell ${status}`} onClick={hitCell}>
            {String.fromCharCode('A'.charCodeAt(0) + x)}{y+1}
        </div>
    );
}