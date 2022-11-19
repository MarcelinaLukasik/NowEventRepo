import React, { Component } from "react";
import Data2 from "./Data2";

//const header = new Headers({ "Access-Control-Allow-Origin": "*" });


class FetchApi2 extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: null
        }

    }

    loadProfiles() {
        fetch('main/2')
            .then(response => response.json())
            .then(data => this.setState({ data: data }))
    }

    componentWillMount() {
        this.loadProfiles()
    }

    render() {
        return (
            <div>
            <h1>Fetch Api</h1>
            {this.state.data && <Data2 user={this.state.data} />}
    </div>
    )
}
}

export default FetchApi2;