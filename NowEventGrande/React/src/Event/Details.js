import SideBar from "./SideBar";
import Carousel from "./Carousel";
import React from 'react';
import {useLocation} from 'react-router-dom';
import { useState, useEffect } from "react";
import {handleStyle} from "./HandleProgress";


function Details(){
    const location = useLocation();
    const eventId = location.state.EventId;
    const [count, setCount] = useState(0);
    

    useEffect(() =>{   
        fetchProgress();
    }, [])

    useEffect(() => {
      handleStyle(count);
    }, [count]);

    async function fetchProgress() {
      const res = await fetch(`/progress/${eventId}/GetChecklistProgress`);      
      res
        .json()
        .then(res => setCount(res));
    }

    return (
        <div className="event"> 
            <div className="row">
                <div className="Event-col-12">    
                    <div className="progressBarContainer"> 
                        <h3 className="progressText" >Checklist progress:</h3>  
                            <div className="progress" id="progress">
                                <div className="progress-bar" id="progress-bar"></div>
                            </div> 
                    </div>
                </div>
            </div>     
            <h1>Details</h1> 

            <div className="row">
                <div className="Event-col-3">
                    <div className="sidebar">
                      <SideBar eventId={eventId}/>
                    </div>
                </div>
                <div className="Event-col-1"> 
                   
                   </div> 
                <div className="Event-col-4">  
                    <Carousel/>
                </div>
            </div>
        </div>

    )

}

export default Details;