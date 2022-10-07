<template>
    <q-btn :class="$attrs.class" label="Выйти" @click="logout" />
</template>

<script>
import { defineComponent } from "vue";
import { fetchlogout } from "boot/axios";

export default defineComponent({
    name: "logout-button",
    props: {
        logined: {
            type: Boolean
        }
    },
    methods: {
        logout() {
            try {
                fetchlogout();
                this.$emit("unautorize");
                this.$router.push({ name: 'homepage' });
            } catch (e) {
                console.log(e.message);
            }
        },
    },
});
</script>
