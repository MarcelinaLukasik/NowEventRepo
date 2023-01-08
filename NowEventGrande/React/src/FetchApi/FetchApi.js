import React, { Component } from "react";
import Data from "./Data";


class FetchApi extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: null
        }

    }

    loadProfiles() {
        fetch('main')
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
            {this.state.data && <Data user={this.state.data} />}
    </div>
    )
}
}

export default FetchApi;