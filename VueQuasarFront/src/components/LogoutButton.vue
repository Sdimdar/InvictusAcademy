<template>
    <q-btn flat label="Выйти" @click="logout" />
</template>

<script>
import defineComponent from 'vue'
import axios from 'axios'

export default defineComponent({
    name: 'logout-button',
    methods:{
        logout: function()
        {
            let config = {
                headers: {
                "Access-Control-Allow-Methods": "GET, POST",
                "Access-Control-Allow-Origin": "*"
                }
            }
            axios.post("https://localhost:7243/Account/LogOff", {}, config)
            .then(ret =>{
                console.log("ok");
            })
            .catch(ret =>{
                console.log("ret");
            })
        }
    }
})
</script>