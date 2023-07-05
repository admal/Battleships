import { useState } from 'react';
import { GridCell } from "../grid/GridCell";
import { Grid } from "../grid/Grid";

export function Game() {
    const [game, setGame] = useState(null);
    const [cells, setCells] = useState([]);

    function onGameFinished() {
        alert("Game finished!");
    }

    function onShipSunken(ship) {
        alert(`Ship sunken: ${ship}`);
    }

    async function startGame() {
        const response = await fetch(`api/game`, {
            method: "POST"
        });

        if (!response.ok) {
            throw new Error(`Something went wrong, couldn't start game`);
        }

        const game = await response.json();

        let cells = [];
        for (var y = 0; y < game.gridHeight; y++) {
            for (var x = 0; x < game.gridWidth; x++) {
                cells.push((<GridCell key={x * 10 + y}
                    gameGuid={game.gameGuid}
                    x={x} y={y}
                    onGameFinished={onGameFinished}
                    onShipSunk={onShipSunken}
                />));
            }
        }

        setCells(cells);
        setGame(game);

    }

    return (
        <div>
            <h1>Welcome to my app</h1>
            {game !== null &&
                <Grid sizeX={10} sizeY={10}>
                    {cells.map(cell => cell)}
                </Grid>
            }
            {game === null && <button onClick={startGame}>Start game</button>}
        </div>
    );
}