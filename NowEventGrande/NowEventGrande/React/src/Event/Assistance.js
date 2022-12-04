import React from 'react';
import lightbulb_1 from '../images/icons/light_bulb_small.png';
import { useState, useEffect } from "react";
import EventInfoPanel from "./EventInfoPanel";

function Assistance(props) {
    const [isOpen, setOpen] = useState(false);
    const openInput = () => {setOpen(!isOpen)};
    const [placeDetails, setPlaceDetails] = useState();

    function getInfo(){
        openInput();
    }

    useEffect(() => {
        fetchVerification();
      }, []);



    async function fetchVerification() {
        const res = await fetch(`/location/${props.eventId}/GetVerificationInfo`);
        const dataJ = await res.json();
        setPlaceDetails(dataJ);
        const result = dataJ;
        return result;
    }


    return (
        <div>
          <img src={lightbulb_1} alt="img" className="assistanceIcon" onClick={getInfo}/>
          {
              isOpen && <EventInfoPanel placeDetails={placeDetails} />
          }                  
        </div>
      );
    
}

export default Assistance;