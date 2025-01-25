VAR recipe_correct = false
VAR recipe_wrong = false

-> Day1

=== Day1 ===
-> sec1


=== sec1 ===

#speaker: Jeremy
"persiapan sudah beres dan waktunya buka tok.."

#sfx: belldor

#speaker: Jeremy
"......."

#sfx: stepboots

#speaker: Alex
"Jer Jer Masih Sepi aja ni toko"

#speaker: Jeremy
"Gimana Gak Sepi Orang baru buka toko"

#speaker: Jeremy
"Lu Nya aja datang kepagian Kocak"

#speaker: Jeremy
"sabar Jeremy sabar(Menghela nafas)"

#speaker: Jeremy
"jangan sampe sifat lu yang  ini ketauan pelanggan pelanggan"

#speaker: Jeremy
"Tumben Juga lu Masih pagi ke siang udah disini"

#speaker: Jeremy
"Biasanya juga lu kan bareng cewe lu sore sore datengnya"

#speaker: Alex
#animation: dissapointed
"hah(menhela nafas)"

#speaker: Alex
#animation: dissapointed
"iya ni jer si jessica, lagi ngambek tadi pagi" 

#speaker: Alex
#animation: dissapointed
"ya masa cuman gara gara nolongin orang balikin dompet pas jogging" 

#speaker: Alex
#animation: dissapointed
"disebut mata keranjang ama jessica" 

#speaker: Jeremy
"yang punya dompetnya cewe?"

#speaker: Alex
"iya cewe" 

#speaker: Jeremy
"cakep?"

#speaker: Alex
#animation: Doubt
"ya...." 

#speaker: Alex
#animation: Doubt
"iya, cakep" 

#speaker: Jeremy
"......."

#speaker: Alex
#animation: Doubt
"......." 

#speaker: Jeremy
"......."

#speaker: Alex
#animation: Doubt
"......." 

#speaker: Jeremy
"yaudah lu mau apa?"

#speaker: Alex
"yaudah tolong bikinin minuman kesukaan nya si jess" 

#speaker: Alex
"kalo gak salah dia tuh suka Thai Tea Cincau Oreo" 

#speaker: Jeremy
"(Menghela nafas)Thai Tea Bobba Oreo, lu gimana si jadi cowo"

#speaker: Alex
"ya yaituh tolong bikinin" 

#speaker: Alex
"Moga aja di reda marah nya" 

#speaker: Jeremy
"Thai Tea Bobba Oreo OTW"

-> check_recipe

=== check_recipe ===
{ 
    - recipe_correct: 
        -> correct_recipe_path
    - recipe_wrong:
        -> wrong_recipe_path
    - else:
        -> check_recipe
}

=== wrong_recipe_path ===

#speaker: Alex
#animation: Dissapointed
"Jer Jer lu pengen gue putus atau gimana" 

#speaker: Alex
#animation: Dissapointed
"Udah Jelas jelas ini bukan Thai Tea Bobba Oreo" 

#speaker: Jeremy
"ah lu juga lupa kesukaan cewe lu apa"

#speaker: Alex
#animation: Dissapointed
"Ya gimana ya, wong gue lupa" 

#speaker: Alex
#animation: Dissapointed
"yaudah lah udah males debat gue" 

#speaker: Alex
#animation: Dissapointed
"tolong bikinin lah yang bener" 

#speaker: Jeremy
"iya iya dibikin lagi"

-> check_recipe
=== correct_recipe_path ===

#speaker: Jeremy
"nih, Thai Tea Bobba Oreo"

#speaker: Alex
#animation: Relief
"yah moga aja amarahnya reda dikasih ini" 

#speaker: Jeremy
"ya moga aja, cewe gak ada yang tau"

#speaker: Alex
#animation: Relief
"yowes kukasih sekarang lah, gue berangkat dulu ya jer" 

#speaker: Alex
"Kubayar nanti ya, abis marahnya reda" 

#speaker: Jeremy
"lu kalo putus/gak putus tetep bayar ya"

#speaker: Alex
"iya lah, tapi janga doa in itu juga astaga" 

#speaker: Alex
"yasudah thank you. duluan" 

#sfx: belldor



#sfx: belldor

#speaker: Jeremy
"selamat datang ka, ada yang bisa dibantu"

"hari hari pun berjalan seperti biasa"

-> END