<template>
    <q-btn :class="$attrs.class" label="Выйти" @click="logout" />
</template>

<script>
import { defineComponent } from "vue";
import axios from 'axios'

export default defineComponent({
    name: 'logout-button',
    props: {
        logined: {
            type: Boolean,
            required: true
        }
    },
    methods:{
        logout: function()
        {
            let config = {
                headers: {
                    "Content-Type": "application/json",
                },
                withCredentials: true
            }
            axios.post("https://localhost:7243/Account/LogOff", {}, config)
            .then(ret =>{
                console.log("ok");
                this.$emit('unautorize');
            })
            .catch(ret =>{
                console.log("ret");
            })
        }
    }
})
</script>