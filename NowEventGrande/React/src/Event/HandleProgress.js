export function handleStyle(progressLevel) {
    const progressbar = document.getElementById("progress-bar");
    if (progressLevel === 0){
      progressbar.style.width = "0%";
      progressbar.style.background = "#ee7551";
    }
    else if (progressLevel === 1){
      progressbar.style.width = "25%";
      progressbar.style.background = "#fad56e";
    }
    else if (progressLevel === 2){
      progressbar.style.width = "50%";
      progressbar.style.background = "#fad56e";
    }
    else if (progressLevel === 3){
      progressbar.style.width = "100%";
      progressbar.style.background = "rgb(129, 219, 94)";
    }      
}
