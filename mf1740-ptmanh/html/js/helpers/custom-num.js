{
    const customNum = document.querySelectorAll('.custom-nums');
    customNum.forEach(num =>{
        const numInput = num.querySelector('.number-input');
        const arrUp = num.querySelector('.arr-up');
        const arrDown = num.querySelector('.arr-down');   
        // add click event to arr up
        arrUp.addEventListener('click', () =>{
            numInput.stepUp();
        })
        // add click event to arr down
        arrDown.addEventListener('click', () =>{
            numInput.stepDown();
        })
    })
}