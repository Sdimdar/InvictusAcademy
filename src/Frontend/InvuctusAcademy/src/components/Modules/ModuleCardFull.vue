<template>
    <div>
        <q-card class="my-card" flat bordered>
            <q-card-section horizontal class="flex justify-between">
                <q-card-section class="q-pt-xs">
                    <div class="text-h5 q-mt-sm q-mb-xs">{{ data.title }}</div>
                    <div class="text-caption text-grey">
                        {{ data.shortDescription }}
                    </div>
                    <div>

                    </div>
                </q-card-section>

                <q-card-section class="col-5 flex flex-center">
                    <q-img class="rounded-borders course-img" src="https://cdn.quasar.dev/img/parallax2.jpg" />
                </q-card-section>
            </q-card-section>
        </q-card>
    </div>
</template>

<script>

export default {
    props: {
        data: []
    }
}
</script>

<style lang="scss" scoped>
    .course-img{
        height: 100px;
        width: 500px;
    }
</style>
