import React from 'react';
import {useLocation} from 'react-router-dom';
import { useState, useEffect } from "react";
import {handleStyle} from "./HandleProgress";


function ProgressBar({fetchCurrentProgress, setFetchCurrentProgress}){
    const location = useLocation();
    const eventId = location.state.EventId;
    const [count, setCount] = useState(0);
    

    useEffect(() =>{   
        fetchProgress();
    }, [])

    useEffect(() =>{   
        fetchProgress();
    }, [fetchCurrentProgress])

    useEffect(() => {
      handleStyle(count);
    }, [count]);

    async function fetchProgress() {
      const res = await fetch(`/progress/${eventId}/GetChecklistProgress`);      
      res
        .json()
        .then(res => setCount(res), setFetchCurrentProgress(false));
    }

    return (          
        <div className="progressBarContainer"> 
            <h3 className="progressText" >Checklist progress:</h3>  
                <div className="progress" id="progress">
                    <div className="progress-bar" id="progress-bar"></div>
                </div> 
        </div>              
    )

}

export default ProgressBar;