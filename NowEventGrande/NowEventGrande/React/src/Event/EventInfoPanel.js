import React from 'react';
import { useState } from "react";

function EventInfoPanel(props) {

    return (
        <div>
          <p>Hello! Let me give you some (hopefully) useful information about your event!</p>
          <p>{props.placeDetails.PlaceStatus}</p>
        </div>
      );
}

export default EventInfoPanel;