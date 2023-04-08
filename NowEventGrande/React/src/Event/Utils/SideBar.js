import React from "react";
import { Outlet, NavLink } from "react-router-dom";
import "../../styles/sidebar.css";
import { Features } from "../Main/Features";

function SideBar(props) {
  return (
    <div className="vertical-menu">
      <ul>
        {Array.from(Features).map((feature, i) => {
          return (
            <li key={i}>
              <NavLink
                to={{
                  pathname: `/event/${props.eventId}/${feature.EndPointName}`,
                }}
                state={{ EventId: props.eventId }}
              >
                {feature.Title}
              </NavLink>
            </li>
          );
        })}
      </ul>
      <Outlet />
    </div>
  );
}

export default SideBar;
