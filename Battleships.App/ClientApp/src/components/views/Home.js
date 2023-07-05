import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>Hello Guestline!</h1>
                <p>Welcome to my take on Battleships challenge!</p>
                <NavLink tag={Link} className="text-dark" to="/game">Start game!</NavLink>
            </div>
        );
    }
}
