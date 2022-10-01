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
        },
        loginedUserEmail: {
            type: String
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
            axios.post("https://localhost:7210/User/Logout", {}, config)
            .then(ret =>{
                this.$emit('unautorize');
            })
            .catch(ret =>{
                console.log("ret");
            })
        }
    }
})
</script>